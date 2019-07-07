import { Container } from "inversify";
import { SampleApiModule } from "./dependency-injection";
import { IQueries, TYPES as SampleServices } from "sample-services";

export * from "./dependency-injection";

export async function doTestAsync() {
    try {

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
        const records = await queries.getSomethingsAsync();
        for(const record in records) {
            console.log(record);
        }
    
    } catch (e) {
        console.log("Encountered error during 'doTestAsync': ", e);
    }
}

(async () => {
    await doTestAsync();
})();
