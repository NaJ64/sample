import { JsonConfigurationLoader } from "config-loader";
import { SampleOrmPersistenceModule } from "sample-infrastructure-persistence-orm";

export async function performMigrationsAsync() {

    const postgresConfig = new JsonConfigurationLoader().load().get("postgres");

    const connectionOptions = SampleOrmPersistenceModule.createPostgresConnectionOptions(postgresConfig);
    
    console.log(connectionOptions);

}

(async () => {
    await performMigrationsAsync();
})();
