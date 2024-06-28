# Our-City BE and Back-Office Application

Description 

# Technical

Our-City is Blazor WASM  Hosted project with .Net version ^8.0 SDK

## Quickstart

This setup is compatible only with UNIX systems such as Linux or MacOS.
Windows WSL2 is supported only with some limitations.

Application run in OCI-container and is compatible with podman or docker as well.

### Prerequisuites

You need to install some stuff.

#### Docker

- docker >= 20.10.17
- docker-compose >= 1.29.2

#### Dot.NET

- .NET >= 8.x.x -> https://dotnet.microsoft.com/en-us/download
- EF (Entity Framework Core cli) >= 8.x.x -> https://learn.microsoft.com/en-us/ef/core/cli/dotnet

### How to start

```
# DB start
docker compose up database

# Export ENV for DotNet core
# P.S. If you use Rider IDE it should be set up automatically depends on your run configuration
export ASPNETCORE_ENVIRONMENT=LocalDevelopment

# Set Up SSL cerifikat for HTTPS
# P.S. If you use Rider IDE it should be set up automatically depends on your run configuration
sudo dotnet dev-certs https
sudo dotnet dev-certs https --check
sudo dotnet dev-certs https --trust

# Set Up EF scripts permissions
bash ./scripts/set_up_permissions.sh

# EF DB create migration -> DON'T NEED TO RUN IT, ONLY FOR INFO
sudo ./scripts/entity-framework/ef_add_migration.sh init

# EF DB update database
./scripts/entity-framework/ef_database_update.sh 0        -- for delete your current DB
./scripts/entity-framework/ef_database_update.sh <NAME_OF_THE_MIGRATION>     -- sync your Entities with DB

# Run in VSC
Simple press `F5` on your keyboard

# Run it on the console
cd srk.salus/
dotnet run --environment "LocalDevelopment"
```

### Migrations

EF DB export production SQL script
./scripts/entity-framework/ef_generate_sql_script.sh <MIGRATION_ID>

Migration Id is only for Migration - update production DB
- For example 1 (recommended): ./scripts/entity-framework/ef_generate_sqlscript.sh <FROM> <TO>
    - ./scripts/entity-framework/ef_generate_sqlscript.sh <YOUR_MIGRATION_NAME> <YOUR_MIGRATION_NAME>
- For example 2 (use the timestemp from migration): ./scripts/entity-framework/ef_generate_sqlscript.sh 20210216094842_init

Take Migration Id from `DB` -> _EFMigrationsHistory - MigrationsId

### Blazor Debugger

Shift+Alt+D in Chromium based browsers.
Then execute the command below in your Terminal.
**macOS**: google-chrome --remote-debugging-port=9222 --user-data-dir=/tmp/blazor-chrome-debug https://localhost:5001/
**Linux**: chromium --remote-debugging-port=9222 --user-data-dir=/tmp/blazor-chrome-debug https://localhost:5001/

In new opened browser press Shift+Alt+D to open the Debugger.
Then go to Source -> file:// -> Client
There you can set breakpoints as you like.
That is Browser debugging. You can use it for debugging JS as well.

### How to use Hot Chocolate with Strawberry Shake

1. Create an Query Endpoint as needed in the Server
2. Go into the `client`-Folder and type `dotnet graphql update` (make sure the Project is running, since you need the schema!)
3. Create the corresponding Query-File in the client

### Existing Environments

#### LocalDevelopment

- DB on localhost:5432 -> docker
- Application on https://localhost:5001

#### Development

Only for development purposes under private network.

- Application on https://ourcity-development.substring.company

#### Testing

- Application on https://ourcity-testing.substring.dev

## Deployment

Per GitLab Pipeline and Kubernetes.
ash salus_deployment.sh <PROJECT_VERSION_TAG>

### Tagging

We use the semantic tagging convention.
Tags are created per GitLab UI.

## Health Check

Every environment has health check endpoint **/_health**
The Health Check provides information about the status of the database.

---

## CI/CD

We use generic pipelines for deployment. Passwords (e.g. DB Logins) are in Gitlab Variables.
and will be injected into the pipeline.

### GitLab Permissions

1. Check the permission of script file for GitLab CI/CD
```bash
git ls-files --stage
```
2. If the permission bits are 644 then enable execute permission from cli
```bash
git update-index --chmod=+x <PATH_TO_YOUR_SCRIPT_FILE>
git commit -m "Adding Executable"
git push origin <YOUR_BRANCHE_NAME>
```

## Role

- **Admin**: Has access to the whole application per BackOffice client
- **User**: Manage a mobile organisation per BackOffice client by permissions

## Permissions

Describe the permissions here.

## Authentication

Authentication is done via JWT and only for BackOffice client.

---

# Graphql Playground

## LocalDevelopment

https://localhost:5001/graphql/

# Swagger

## LocalDevelopment

JSON - `http://0.0.0.0:5001/swagger/v1/swagger.json`

Normal - `http://0.0.0.0:5001/swagger/index.html`

---

# Formatting

In root project folder run the following command to format the code:

```bash
dotnet csharpier .
```

https://plugins.jetbrains.com/plugin/18243-csharpier

https://csharpier.com/

https://dev.to/tsotsi1/dotnet-c-code-format-on-jetbrain-ide-rider-504i

---

# Authors

- [Michal Durik](https://github.com/miko866)

# Copyright

&copy; OurCity s.r.o.

# Contact

[mdurik2@gmail.com](mailto:mdurik2@gmail.com)

[ourcity-testing.substring.dev/](https://ourcity-testing.substring.dev/)