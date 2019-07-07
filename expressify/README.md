# `expressify`

> Mimics web hosting patterns found in ASP.NET Core by bundling Express and Inversify framework(s)

## Usage

```
import "reflect-metadata";  // Reflect api polyfill (needed by inversify)
import { IWebHostBuilder, WebHost } from "expressify";
import { Startup } from "./startup"; // reference startup-like class for convention-based configuration

export class Program {

    public static main(...args: any[]): void {
        Program.createWebHostBuilder(args).build().run();
    }

    public static createWebHostBuilder(...args: any[]): IWebHostBuilder {
        return WebHost.createDefaultBuilder(args)
            .useStartup(Startup);
    }

}
```

```
import express from "express";
import { Container } from "inversify";

export class Startup {

    private readonly _configuration: Map<string, any>;
    private readonly _environment: string;

    public constructor(configuration: Map<string, any>, environment: string) {
        this._configuration = configuration;
        this._environment = environment;
    }

    public configureServices(services: Container) {
        if (this._environment == "dev") {
            services.bind("postgresConfig")
                .toConstantValue(this._configuration.get("postgres"));
        }
    }

    public configure(app: express.Application, services: Container, env: string): express.Application {
        return app.get('/', async (req, res) => {
            try {
                if (env == "dev") {
                    const postgresConfig = services.get("postgresConfig");
                    res.send(JSON.stringify(postgresConfig));
                } else {
                    res.send("Hello world!");
                }
                
            } catch(e) {
                console.log("Encountered error: ", e)
                res.send(JSON.stringify(e));
            }
        });
    }

}
```