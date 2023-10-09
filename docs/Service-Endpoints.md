# Swagger Description

## Endpoints

### /api/Blog/Blogs

HTTP Method: post
Anonymous:   True

Request:     #/components/schemas/Nucleus.Blog.Contracts.Models.Filters.BlogsFilter

### /api/Blog/Slug/{id}

HTTP Method: get
Anonymous:   True



### /api/Blog/RecentBlogs/{id}

HTTP Method: get
Anonymous:   True



### /api/Blog/Query

HTTP Method: post
Anonymous:   True

Request:     #/components/schemas/Eliassen.System.Linq.Search.SearchQuery-Nucleus.Blog.Contracts.Models.BlogModel

Response: #/components/schemas/Eliassen.System.ResponseModel.PagedQueryResult-Nucleus.Blog.Contracts.Models.BlogModel

### /api/BlogAdmin/Blogs

HTTP Method: post
Anonymous:   False
Rights:
* right_blog_admin

Request:     #/components/schemas/Nucleus.Blog.Contracts.Models.Filters.BlogsFilter

### /api/BlogAdmin/Blog/{id}

HTTP Method: get
Anonymous:   False
Rights:
* right_blog_admin



### /api/BlogAdmin/Save

HTTP Method: post
Anonymous:   False
Rights:
* right_blog_admin

Request:     #/components/schemas/Nucleus.Blog.Contracts.Models.BlogModel

### /api/Documents/{id}

HTTP Method: get
Anonymous:   False


HTTP Method: delete
Anonymous:   False
Rights:
* right_lesson_admin

* right_blog_admin

* right_project_admin



Response: #/components/schemas/Nucleus.Core.Contracts.Models.ResponseModel-System.Boolean

### /api/Documents

HTTP Method: post
Anonymous:   False
Rights:
* right_lesson_admin

* right_blog_admin

* right_project_admin



Response: #/components/schemas/Nucleus.Core.Contracts.Models.ResponseModel-Nucleus.External.Azure.StorageAccount.DocumentModel

### /api/Lesson/Lessons

HTTP Method: post
Anonymous:   True

Request:     #/components/schemas/Nucleus.Lesson.Contracts.Models.Filters.LessonsFilter

### /api/Lesson/Slug/{id}

HTTP Method: get
Anonymous:   True



### /api/Lesson/RecentLessons/{id}

HTTP Method: get
Anonymous:   True



### /api/Lesson/Query

HTTP Method: post
Anonymous:   True

Request:     #/components/schemas/Eliassen.System.Linq.Search.SearchQuery-Nucleus.Lesson.Contracts.Models.LessonModel

Response: #/components/schemas/Eliassen.System.ResponseModel.PagedQueryResult-Nucleus.Lesson.Contracts.Models.LessonModel

### /api/LessonAdmin/Lessons

HTTP Method: post
Anonymous:   False
Rights:
* right_lesson_admin

Request:     #/components/schemas/Nucleus.Lesson.Contracts.Models.Filters.LessonsFilter

### /api/LessonAdmin/Lesson/{id}

HTTP Method: get
Anonymous:   False
Rights:
* right_lesson_admin



### /api/LessonAdmin/Save

HTTP Method: post
Anonymous:   False
Rights:
* right_lesson_admin

Request:     #/components/schemas/Nucleus.Lesson.Contracts.Models.LessonModel

### /api/Project/Projects

HTTP Method: post
Anonymous:   False

Request:     #/components/schemas/Nucleus.Project.Contracts.Models.Filters.ProjectFilter

### /api/Project/Slug/{id}

HTTP Method: get
Anonymous:   False



### /api/Project/RecentProjects/{id}

HTTP Method: get
Anonymous:   False



### /api/ProjectAdmin/Projects

HTTP Method: post
Anonymous:   False
Rights:
* right_project_admin

Request:     #/components/schemas/Nucleus.Project.Contracts.Models.Filters.ProjectFilter

### /api/ProjectAdmin/Project/{id}

HTTP Method: get
Anonymous:   False
Rights:
* right_project_admin



### /api/ProjectAdmin/Save

HTTP Method: post
Anonymous:   False
Rights:
* right_project_admin

Request:     #/components/schemas/Nucleus.Project.Contracts.Models.ProjectModel

### /api/RegisterUser

HTTP Method: post
Anonymous:   True

Request:     #/components/schemas/Nucleus.Core.Contracts.Models.UserAction

Response: #/components/schemas/Nucleus.Core.Contracts.Models.ResponseModel-Nucleus.Core.Contracts.Models.User

### /api/RegisterUser/ApplicationPermissions

HTTP Method: get
Anonymous:   True



Response: #/components/schemas/Eliassen.System.ResponseModel.PagedQueryResult-Nucleus.Core.Contracts.Models.Module

### /api/RegisterUser/ApplicationPemissions

HTTP Method: get
Anonymous:   True
Rights:
* right_users_write



Response: #/components/schemas/Nucleus.Core.Contracts.Models.PagedResult-Nucleus.Core.Contracts.Models.Module

### /api/SiteMap

HTTP Method: get
Anonymous:   False



### /api/User

HTTP Method: get
Anonymous:   False



Response: #/components/schemas/Nucleus.Core.Contracts.Models.User
HTTP Method: put
Anonymous:   False

Request:     #/components/schemas/Nucleus.Core.Contracts.Models.User

Response: #/components/schemas/Nucleus.Core.Contracts.Models.ResponseModel-Nucleus.Core.Contracts.Models.User

### /api/UserManagement

HTTP Method: post
Anonymous:   False
Rights:
* right_users_write

Request:     #/components/schemas/Nucleus.Core.Contracts.Models.UserAction

Response: #/components/schemas/Nucleus.Core.Contracts.Models.ResponseModel-Nucleus.Core.Contracts.Models.User

### /api/UserManagement/Query

HTTP Method: post
Anonymous:   False
Rights:
* right_users_write

Request:     #/components/schemas/Eliassen.System.Linq.Search.SearchQuery-Nucleus.Core.Contracts.Models.User

Response: #/components/schemas/Eliassen.System.ResponseModel.PagedQueryResult-Nucleus.Core.Contracts.Models.User

### /api/UserManagement/ApplicationPermissions

HTTP Method: get
Anonymous:   False
Rights:
* right_users_write



Response: #/components/schemas/Eliassen.System.ResponseModel.PagedQueryResult-Nucleus.Core.Contracts.Models.Module

### /api/UserManagement/UserList

HTTP Method: post
Anonymous:   False
Rights:
* right_users_write

Request:     #/components/schemas/Nucleus.Core.Contracts.Models.Filters.UsersFilter

Response: #/components/schemas/Nucleus.Core.Contracts.Models.PagedResult-Nucleus.Core.Contracts.Models.User

### /api/UserManagement/ApplicationPemissions

HTTP Method: get
Anonymous:   False
Rights:
* right_users_write



Response: #/components/schemas/Nucleus.Core.Contracts.Models.PagedResult-Nucleus.Core.Contracts.Models.Module

## Models

### Eliassen.System.Linq.Search.FilterParameter


#### Properties

* eq : object?
* neq : object?
* in : array?
* gt : object?
* gte : object?
* lt : object?
* lte : object?

### Eliassen.System.Linq.Search.OrderDirections


### Eliassen.System.Linq.Search.SearchQuery-Nucleus.Blog.Contracts.Models.BlogModel


#### Properties

* currentPage : integer
* pageSize : integer
* excludePageCount : boolean
* searchTerm : string?
* filter : object?
* orderBy : object?

### Eliassen.System.Linq.Search.SearchQuery-Nucleus.Core.Contracts.Models.Module


#### Properties

* currentPage : integer
* pageSize : integer
* excludePageCount : boolean
* searchTerm : string?
* filter : object?
* orderBy : object?

### Eliassen.System.Linq.Search.SearchQuery-Nucleus.Core.Contracts.Models.User


#### Properties

* currentPage : integer
* pageSize : integer
* excludePageCount : boolean
* searchTerm : string?
* filter : object?
* orderBy : object?

### Eliassen.System.Linq.Search.SearchQuery-Nucleus.Lesson.Contracts.Models.LessonModel


#### Properties

* currentPage : integer
* pageSize : integer
* excludePageCount : boolean
* searchTerm : string?
* filter : object?
* orderBy : object?

### Eliassen.System.ResponseModel.MessageLevels


### Eliassen.System.ResponseModel.PagedQueryResult-Nucleus.Blog.Contracts.Models.BlogModel


#### Properties

* rows : array?
* messages : array?
* currentPage : integer
* totalPageCount : integer
* totalRowCount : integer

### Eliassen.System.ResponseModel.PagedQueryResult-Nucleus.Core.Contracts.Models.Module


#### Properties

* rows : array?
* messages : array?
* currentPage : integer
* totalPageCount : integer
* totalRowCount : integer

### Eliassen.System.ResponseModel.PagedQueryResult-Nucleus.Core.Contracts.Models.User


#### Properties

* rows : array?
* messages : array?
* currentPage : integer
* totalPageCount : integer
* totalRowCount : integer

### Eliassen.System.ResponseModel.PagedQueryResult-Nucleus.Lesson.Contracts.Models.LessonModel


#### Properties

* rows : array?
* messages : array?
* currentPage : integer
* totalPageCount : integer
* totalRowCount : integer

### Eliassen.System.ResponseModel.ResultMessage


#### Properties

* level : Eliassen.System.ResponseModel.MessageLevels
* message : string?
* messageCode : string?
* context : string?
* metaData : object?

### Microsoft.AspNetCore.Mvc.ProblemDetails


#### Properties

* type : string?
* title : string?
* status : integer?
* detail : string?
* instance : string?

### Nucleus.Blog.Contracts.Models.BlogModel


#### Properties

* blogId : string?
* title : string?
* slug : string?
* tags : string[]?
* preview : string?
* content : string?
* enabled : boolean?
* author : string?
* createdOn : string
* createdOnUnix : integer

### Nucleus.Blog.Contracts.Models.Filters.BlogsFilter


#### Properties

* blogFilters : Nucleus.Blog.Contracts.Models.Filters.BlogsFilterItem
* pagingModel : Nucleus.Core.Contracts.Models.PagingModel

### Nucleus.Blog.Contracts.Models.Filters.BlogsFilterItem


#### Properties

* inputValue : string?

### Nucleus.Core.Contracts.Models.Filters.UserFilterItem


#### Properties

* inputValue : string?
* module : string?
* userStatus : string?

### Nucleus.Core.Contracts.Models.Filters.UsersFilter


#### Properties

* userFilters : Nucleus.Core.Contracts.Models.Filters.UserFilterItem
* pagingModel : Nucleus.Core.Contracts.Models.PagingModel

### Nucleus.Core.Contracts.Models.Module


#### Properties

* name : string?
* code : string?
* moduleId : string?
* roles : array?

### Nucleus.Core.Contracts.Models.PagedResult-Nucleus.Core.Contracts.Models.Module


#### Properties

* currentPage : integer
* pageCount : integer
* pageSize : integer
* rowCount : integer
* firstRowOnPage : integer
* lastRowOnPage : integer
* results : array?
* rows : array?
* totalPageCount : integer
* totalRowCount : integer
* messages : array?

### Nucleus.Core.Contracts.Models.PagedResult-Nucleus.Core.Contracts.Models.User


#### Properties

* currentPage : integer
* pageCount : integer
* pageSize : integer
* rowCount : integer
* firstRowOnPage : integer
* lastRowOnPage : integer
* results : array?
* rows : array?
* totalPageCount : integer
* totalRowCount : integer
* messages : array?

### Nucleus.Core.Contracts.Models.PagingModel


#### Properties

* currentPage : integer
* pageSize : integer
* sortBy : string?
* sortDirection : string?
* excludePageCount : boolean

### Nucleus.Core.Contracts.Models.PermissionBase


#### Properties

* name : string?
* code : string?

### Nucleus.Core.Contracts.Models.ResponseModel-Nucleus.Core.Contracts.Models.User


#### Properties

* isSuccess : boolean
* message : string?
* response : Nucleus.Core.Contracts.Models.User

### Nucleus.Core.Contracts.Models.ResponseModel-Nucleus.External.Azure.StorageAccount.DocumentModel


#### Properties

* isSuccess : boolean
* message : string?
* response : Nucleus.External.Azure.StorageAccount.DocumentModel

### Nucleus.Core.Contracts.Models.ResponseModel-System.Boolean


#### Properties

* isSuccess : boolean
* message : string?
* response : boolean

### Nucleus.Core.Contracts.Models.Role


#### Properties

* name : string?
* code : string?
* rights : array?

### Nucleus.Core.Contracts.Models.User


#### Properties

* userId : string?
* userName : string?
* emailAddress : string?
* firstName : string?
* lastName : string?
* active : boolean
* userModules : array?
* createdOn : string?

### Nucleus.Core.Contracts.Models.UserAction


#### Properties

* userId : string?
* userName : string?
* emailAddress : string?
* firstName : string?
* lastName : string?
* active : boolean
* userModules : array?
* createdOn : string?
* identityAction : string?

### Nucleus.Core.Contracts.Models.UserModule


#### Properties

* name : string?
* code : string?
* roles : array?

### Nucleus.External.Azure.StorageAccount.DocumentModel


#### Properties

* documentId : string?
* documentKey : string?
* documentName : string?
* documentType : string?
* documentSize : integer?
* documentRepository : string?
* documentCategory : string?
* createdOn : string?

### Nucleus.Lesson.Contracts.Models.Filters.LessonsFilter


#### Properties

* lessonFilters : Nucleus.Lesson.Contracts.Models.Filters.LessonsFilterItem
* pagingModel : Nucleus.Core.Contracts.Models.PagingModel

### Nucleus.Lesson.Contracts.Models.Filters.LessonsFilterItem


#### Properties

* inputValue : string?

### Nucleus.Lesson.Contracts.Models.LessonModel


#### Properties

* lessonId : string?
* title : string?
* mediaLink : string?
* preview : string?
* previewImage : string?
* slug : string?
* content : string?
* enabled : boolean?
* createdOn : string?
* createdOnUnix : integer
* attendees : string[]?
* teacher : string?
* duration : integer?
* startDateTime : string?
* endDateTime : string?
* tags : string[]?
* price : number
* goals : string[]?
* notes : string?

### Nucleus.Project.Contracts.Models.Filters.ProjectFilter


#### Properties

* projectFilters : Nucleus.Project.Contracts.Models.Filters.ProjectsFilterItem
* pagingModel : Nucleus.Core.Contracts.Models.PagingModel

### Nucleus.Project.Contracts.Models.Filters.ProjectsFilterItem


#### Properties

* inputValue : string?

### Nucleus.Project.Contracts.Models.ProjectModel


#### Properties

* projectId : string?
* title : string?
* projectLink : string?
* projectImage : string?
* slug : string?
* preview : string?
* content : string?
* page : string?
* enabled : boolean?
* createdOn : string
* createdOnUnix : integer

---

Generated: 2023-09-28 23:03:15
