
## API.StartApp - .Net Core Web API App


### Setup
   * Install ``NET Core SDK 2.2``
   * Install/Configure a ``MariaDB >= 10.3`` database (version 10.3 or higher)
   * Clone this repo 
     * Make sure the ``API.Base`` submodule is cloned (see submodule options in your Git GUI tool)
   * Set your environment variables
       * Create ``conf.vars.local`` file and overwrite environment variables from ``conf.vars`` (if necessary)
       * Run ``./set-env-vars.sh`` (Linux) / ``set-env-vars.bat`` (Windows)
    * Update and Seed database: ``dotnet run --seed true --migrate true``

### Dev
   * Run:
        * Linux: ``./run-dev.sh``
        * Windows: ``run-dev.bat``
   * Adding new model+api (don't forget to change the name of the entity in your commands)
        * Add ``CustomEntity`` in ``YourProject/Models/Entity`` inheriting from ``API.Base/API.Base.Web.Base/Models/Entities/Entity``
            * This model is linked to the database table.
        * Add ``CustomViewModel`` in ``YourProject/Models/ViewModels`` inheriting from ``API.Base/API.Base.Web.Base/Models/ViewModels/ViewModel``
            * This ViewModel is used in api for json parsing/serializing.
        * Add ``CustomEntityMap`` in ``YourProject/Models/EntityMaps``
            * This EntityMap links the Entity and the ViewModel in AutoMapper and tells EntityFramework this Entity should be linked into database.
        * Generate Migration: ``dotnet ef migrations add migration_name``
        * Update Database: ``dotnet ef database update``
        
   * New admin UI for new model
     * Check this section in ``API.Base`` Readme. (The ``readme.md`` file in ``API.Base`` directory/repository).   
 
### Deploy/Access
   * access 
        * ``/api/docs`` for swagger 
        * ``/api/[controller]/[action]`` for api
        * ``/api/admin`` for the WIP admin panel
   * if deployed under proxy those paths must be proxied:
     * ``/api*``
     * ``/Identity*``
     
### Environment variables
  * Required
    * ``ASPNETCORE_ENVIRONMENT`` - Development/Production
    * ``CONNECTION_STRING`` - Database connection string
    * ``CONTENT_DIRECTORY`` - path to the directory where uploaded files are stored
    * ``LISTEN_PORT`` - ex: 6969
    * ``JWT_SECURITY_KEY`` - JWT Secret security key for signing JWT tokens
    * ``TEMPORARY_VIEWS_PATH`` - A directory with write access for storing temporary Razor Views files 
  * Optional 
    * ``SENDGRID_KEY`` - SendGrid API Key - for sending emails (if not set the sending is simulated in stdout/console)
    * ``LOGS_DIRECTORY`` - directory to put logs file in, if not set '../logs' will be used
    * ``ALLOWED_CORS_HOSTS`` - ';' separated list of hosts (http[s]://domain[:port])
    * ``VIEWS_AND_WWW_PATHS`` - Only with ``dotnet [watch] run`` - ';' separated list of relative directory paths of projects which includes Views or wwwroot folders 
