# Palindromic

This repo was created as a test for a job interview.

The objective was to develop an application that finds the largest palindromic number between two given numbers (exclusive).

## How to run

You can have a quick check on the functionality by visiting [this link](https://pepenotti.github.io/Palindromic/).

![Preview](./docs/site-preview.png)

1. Set the first number.
2. Set the second number.
3. Click on the `Submit` button trigger the search for the largest palindromic number between the two numbers.
4. The result will be displayed below the form.

If you want to run the application locally, you can follow the instructions below and run it as:
- [Running as Website and Azure Function](#running-as-website-and-azure-function)
- [Running as Console Application](#running-as-console-application)


### Running as Website and Azure Function

#### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/en/download/)
- [Azure Functions Core Tools](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local)

#### Running the Azure Function

1. Open a terminal window.
2. Navigate to the directory containing your Azure Function project:

    ```sh
    cd .\src\Palindromic.Functions
    ```

3. Start the Azure Function locally using the Azure Functions Core Tools:

    ```sh
    func start
    ```

#### Running the Web Application

1. Open another terminal window or tab.
2. Navigate to the directory containing your web application project:

    ```sh
    cd .\src\palindromic.web
    ```

3. Install the dependencies if you haven't already:

    ```sh
    npm install
    ```

4. Start the web application:

    ```sh
    npm run dev -- --host
    ```

### Configuration

Ensure that your web application is configured to communicate with the locally running Azure Function. You can set the `VITE_AZURE_FUNCTION_URL` environment variable to point to the local URL of the Azure Function, typically `http://localhost:7071/api`.

You can set this environment variable in a [.env](./src/palindromic.web/.env) file in your web application directory:

```properties
VITE_AZURE_FUNCTION_URL=http://localhost:7071/api
```

### Running as Console Application

#### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Azure Functions Core Tools](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local)


#### Running the Console Application

1. Open a terminal window.
2. Navigate to the directory containing your console application project:

    ```sh
    cd .\src\Palindromic.Console
    ```
3. Run the console application:

    ```sh
    dotnet run {numberA} {numberB}
    ```
