
REM @REM SET
@ECHO OFF
SETLOCAL

SET TestProject=Nucleus.Acc.Net.Api.sln
REM SET Configuration=Debug
SET Configuration=Release

IF "%1"=="--no-start" (
	SET DO_NOT_START=1
	SHIFT
)
IF "%1"=="--no-run" (
	SET DO_NOT_RUN=1
	SHIFT
)

IF NOT "%1"=="" IF NOT "%TestFilter%"=="" SET TestFilter=

:check_again
IF "%1"=="" GOTO ready_to_run

SET temp_filter=%1
IF "%TestFilter%"=="" (
	SET TestFilter=TestCategory=%temp_filter%
) ELSE (
	SET TestFilter=%TestFilter%^|TestCategory=%temp_filter%
)

SHIFT
GOTO check_again

:ready_to_run
IF "%TestFilter%"=="" SET TestFilter=TestCategory=Unit^|TestCategory=Simulate

ECHO "%TestFilter%"

dotnet tool install --global dotnet-reportgenerator-globaltool 2>NUL

IF '%DO_NOT_RUN%'=='' (
RMDIR /S/Q ".\TestResults" 2>NUL
MKDIR ".\TestResults\Coverage\Reports" 2>NUL

dotnet test "%TestProject%" ^
--configuration %Configuration% ^
--results-directory .\TestResults ^
--nologo ^
--settings .runsettings ^
--filter "%TestFilter%"
)

SET TEST_ERR=%ERRORLEVEL%

REM dotnet pack "Lightwell.FixSourceLinks.Cli" --configuration Release -o "Publish\Nuget"
REM dotnet tool install Lightwell.FixSourceLinks.Cli --add-source=Publish\Binary --version 1.0.0
REM dotnet fix-sourcelink --include=.\TestResults\**\coverage.cobertura.xml --target=..\..
REM dotnet tool uninstall Lightwell.FixSourceLinks.Cli

dotnet dotnet-coverage merge ^
coverage.*.xml ^
--recursive ^
--output .\TestResults\Cobertura.coverage ^
--output-format cobertura

reportgenerator "-reports:.\TestResults\**\coverage.*.xml" "-targetDir:.\TestResults\Coverage\Reports" -reportTypes:HtmlSummary;Cobertura;MarkdownSummary "-title:%TestProject% - (%Configuration%)"

IF '%DO_NOT_START%'=='' (
START .\TestResults\Coverage\Reports\summary.html
START .\TestResults\Cobertura.coverage
START .\TestResults\LatestTestResults.trx
)

ECHO TEST_ERR=%TEST_ERR%
IF "%TEST_ERR%"=="0" (
	ECHO "No Errors :)"
)
ENDLOCAL

