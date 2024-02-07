
@ECHO OFF
SETLOCAL EnableDelayedExpansion

SET SolutionDir=%~dp0
ECHO SolutionDir %SolutionDir%
SET PublishPath=%SolutionDir%publish\libs\
ECHO PublishPath %PublishPath%

SET TARGET_SOLUTION=Eliassen.Libs.sln
SET TARGET_SOLUTION_NAME=Eliassen.Libs
SET TARGET_WEB_PROJECT=.\Examples\Eliassen.WebApi\Eliassen.WebApi.csproj

SET TEMPLATE_COMMAND=run --project Tools\Eliassen.TemplateEngine.Cli
REM SET TEMPLATE_COMMAND=templateengine

ECHO "restore current .net tools"
dotnet tool restore

echo "Git fetch"
git fetch --prune
FOR /F "tokens=* USEBACKQ" %%g IN (`dotnet gitversion /output json /showvariable FullSemVer`) DO (SET BUILD_VERSION=%%g)
if "%BUILD_VERSION%"=="" GOTO error
ECHO Building Version=  "%BUILD_VERSION%"

CALL :FORMAT_SOURCE_CODE

CALL build.bat
SET TEST_ERR=%ERRORLEVEL%
IF NOT "%TEST_ERR%"=="0" (
	ECHO "Build Failed! %TEST_ERR%"
	GOTO :skiptoend
)

CALL :BUILD_SWAGGER_DOCS
CALL :GENERATE_ENDPOINTS_REPORT
CALL :GENERATE_CODE_DOCS
CALL :GENERATE_LIBRARY_DOCS

CALL test.bat --no-start
SET TEST_ERR=%ERRORLEVEL%
IF NOT "%TEST_ERR%"=="0" (
	ECHO "Tests Failed! %TEST_ERR%"
	GOTO :skiptoend
)

CALL :GENERATE_TEST_REPORTS
CALL :GENERATE_SOFTWARE_BOM
CALL :GENERATE_SOFTWARE_BOM_REPORT

ECHO TEST_ERR=%TEST_ERR%
:skiptoend
IF "%TEST_ERR%"=="0" (
	ECHO "No Errors :)"
)
EXIT /B %TEST_ERR%
:EOF
ENDLOCAL
EXIT /B

REM ===============================

EXIT /B

:FORMAT_SOURCE_CODE
dotnet format ^
--verbosity detailed ^
--report %PublishPath%..\reports\format.json
EXIT /B

:BUILD_SWAGGER_DOCS
ECHO "Generate - swagger docs"
dotnet build /T:BuildSwagger %TARGET_WEB_PROJECT%
EXIT /B

:GENERATE_ENDPOINTS_REPORT

ECHO "Generate Service-Endpoints"
dotnet %TEMPLATE_COMMAND% ^
--configuration Release ^
--input .\docs\swagger.json ^
--output .\docs\Service-Endpoints.md ^
--Template Service-Endpoints ^
--file-template-path .\docs\templates
EXIT /B

:GENERATE_CODE_DOCS
ECHO "Generate - Library code docs"
RMDIR .\docs\code /S/Q
dotnet build /T:GetDocumentation
EXIT /B

:GENERATE_LIBRARY_DOCS
ECHO "Generate - Library Docs"
RMDIR .\docs\Libraries /S/Q
dotnet %TEMPLATE_COMMAND% ^
--configuration Release ^
--input %PublishPath%*.xml ^
--output .\docs\Libraries\[file].md ^
--Template Documentation.md ^
--file-template-path .\docs\templates
SET TEST_ERR=%ERRORLEVEL%
IF NOT "%TEST_ERR%"=="0" (
	ECHO "SBOM Failed! %TEST_ERR%"
    EXIT /B %TEST_ERR%
)
DEL .\docs\Libraries\Microsoft*.* /Q
EXIT /B

:GENERATE_TEST_REPORTS
ECHO "Generate - Test Docs"
RMDIR .\docs\Tests /S/Q
MKDIR .\docs\Tests
ECHO "Copy Code Coverage Results"
COPY .\TestResults\Cobertura.coverage .\docs\Tests\Cobertura.coverage /Y
ECHO "Copy Code Test Results"
COPY .\TestResults\Coverage\Reports\LatestTestResults.trx .\docs\Tests\LatestTestResults.trx /Y
ECHO "Copy Code Coverage Report"
COPY .\TestResults\Coverage\Reports\Summary.md .\docs\Tests\Summary.md /Y
ECHO "Generate - Test Result"
dotnet %TEMPLATE_COMMAND% ^
--configuration Release ^
--input .\TestResults\Coverage\Reports\*.trx ^
--output .\docs\Tests\[file].md ^
--Template TestResultsToMarkdown.md ^
--file-template-path .\docs\templates ^
--input-type XML
SET TEST_ERR=%ERRORLEVEL%
IF NOT "%TEST_ERR%"=="0" (
	ECHO "SBOM Failed! %TEST_ERR%"
    EXIT /B %TEST_ERR%
)
EXIT /B

:GENERATE_SOFTWARE_BOM
ECHO "Generate - Software Bill of Materials (bom.xml)"
RMDIR .\docs\sbom /S/Q
REM https://github.com/CycloneDX/cyclonedx-dotnet
dotnet CycloneDX ^
--output .\docs\sbom ^
--set-version %BUILD_VERSION% ^
--set-name %TARGET_SOLUTION_NAME% ^
--exclude-test-projects ^
--disable-package-restore ^
--exclude-dev ^
%TARGET_SOLUTION%
SET TEST_ERR=%ERRORLEVEL%
IF NOT "%TEST_ERR%"=="0" (
	ECHO "SBOM Failed! %TEST_ERR%"
    EXIT /B %TEST_ERR%
)
EXIT /B

:GENERATE_SOFTWARE_BOM_REPORT
ECHO "Generate - Software Bill of Materials (report)"
dotnet %TEMPLATE_COMMAND% ^
--configuration Release ^
--input .\docs\sbom\bom.xml ^
--output .\docs\sbom\BillOfMaterials.md ^
--Template SoftwareBillOfMaterials.md ^
--file-template-path .\docs\templates ^
--input-type XML
SET TEST_ERR=%ERRORLEVEL%
IF NOT "%TEST_ERR%"=="0" (
	ECHO "SBOM Failed! %TEST_ERR%"
    EXIT /B %TEST_ERR%
)
EXIT /B