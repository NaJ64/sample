import { Container } from "inversify";
import { SampleApiModule } from "./dependency-injection";
import { IQueries, TYPES as SampleServices } from "sample-services";

export * from "./dependency-injection";

export async function doTestAsync() {
    
    const container = new Container();
    container.load(new SampleApiModule(options => {
        options.domainCommands = true;
        options.domainQueries = true;
        options.postgresConnection = {
            host: "localhost",
            port: 3306,
            username: "test",
            password: "test",
            database: "test"
        };
    }));

    const queries = container.get<IQueries>(SampleServices.IQueries);
    const records = queries.getSomethingsAsync();
    for(const record in records) {
        console.log(record);
    }
    
}

(async () => {
    await doTestAsync();
})();
