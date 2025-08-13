## Steps to initialize the solution
* Create a new solution 

```bash
dotnet new sln -n DotnetTraining
```

## Steps to add a new project to the solution
* create project folder
```bash
mkdir -p src/CookiesDemo
```

* Create a new project
```bash
dotnet new razor -o src/CookiesDemo
```

* Add project to solution
```bash
dotnet sln add src/CookiesDemo/CookiesDemo.csproj --in-root
```

* Check if project is added to solution
```bash
dotnet sln list
```

## Steps to run the project
* Build the project (restore packages and check if compiles)
```bash
dotnet build src/CookiesDemo/CookiesDemo.csproj
```

* Run the project
```bash
dotnet run --project src/CookiesDemo/CookiesDemo.csproj
```