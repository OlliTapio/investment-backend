# Investment-backend

This is the back for the team "Finnish-Swiss-Connection", using the Nordea Open Banking API.

## Prerequisites

- DotNetCore 2.0 [Link](https://www.microsoft.com/net/)

## Restore & Run
The project uses the dotnet watch for live development.

```
dotnet restore
dornet watch run
```

## API Endpoint

| Endpoint | Return |
|-|-|
|api/Portfolio/account| acount{AvailableBalance, BookedBalance}|