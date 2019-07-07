import express from "express";
import { Container } from "inversify";
import { IQueries, TYPES as SampleServices } from "sample-services";
import { SampleApiModule } from "./dependency-injection/container-module";

export class Startup {

    private readonly _configuration: Map<string, any>;
    //private readonly _environment: string;

    public constructor(configuration: Map<string, any>, environment: string) {
        this._configuration = configuration;
        //this._environment = environment;
    }

    public configureServices(services: Container) {
        const postgresConnection = this._configuration.get("postgres");
        services.load(new SampleApiModule(options => {
            options.domainCommands = true;
            options.domainQueries = true;
            options.postgresConnection = postgresConnection;
        }));
    }

    public configure(app: express.Application, services: Container, env: string): express.Application {

        app.get('/', async (req, res) => {
            try {
                
                const queries = services.get<IQueries>(SampleServices.IQueries);
                const records = await queries.getSomethingsAsync();
                res.send(JSON.stringify(records));
                
            } catch(e) {
                console.log("Encountered error: ", e)
                res.send(JSON.stringify(e));
            }
        });

        return app;

    }

}