import express from "express";
import { Container } from "inversify";
import { IQueries, TYPES as SampleServices, IAddSomethingHandler, IUpdateSomethingHandler, UpdateSomethingCommand, AddSomethingCommand } from "sample-services";
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
            options.postgres = postgresConnection;
        }));
    }

    public configure(app: express.Application, services: Container, env: string): express.Application {

        app.get('/', async (req, res) => {
            try {
                
                const queries = services.get<IQueries>(SampleServices.IQueries);
                let records = await queries.getSomethingsAsync();
                if (records.length) {
                    const firstSomething = records[0];

                    const updateSomethingHandler = services.get<IUpdateSomethingHandler>(SampleServices.Commands.IUpdateSomethingHandler);
                    await updateSomethingHandler.handleAsync(new UpdateSomethingCommand({
                        someId: firstSomething.someId,
                        someNewData: firstSomething.someData + ' | ' + new Date().toISOString()
                    }));

                    // const removeSomethingHandler = services.get<IRemoveSomethingHandler>(SampleServices.Commands.IRemoveSomethingHandler);
                    // await removeSomethingHandler.handleAsync(new RemoveSomethingCommand({
                    //     someId: firstSomething.someId
                    // }));
                } else {
                    const addSomethingHandler = services.get<IAddSomethingHandler>(SampleServices.Commands.IAddSomethingHandler);
                    await addSomethingHandler.handleAsync(new AddSomethingCommand({
                        someNewData: new Date().toISOString()
                    }));
                }

                records = await queries.getSomethingsAsync();
                res.send(JSON.stringify(records));
                
            } catch(e) {
                console.log("Encountered error: ", e)
                res.send(JSON.stringify(e));
            }
        });

        return app;

    }

}