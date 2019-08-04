import { JsonConfigurationLoader } from "config-loader";
import { SampleOrmPersistenceModule } from "sample-infrastructure-persistence-orm";
import { ConnectionOptions } from "typeorm";

const postgresConfig = new JsonConfigurationLoader().load().get("postgres");
let connectionOptions: ConnectionOptions = SampleOrmPersistenceModule.createPostgresConnectionOptions(postgresConfig);
connectionOptions = Object.assign(
    connectionOptions, 
    {
        migrationsTableName: "__TypeORMMigrationsHistory",
        migrations: ["dist/typeorm/migrations/*.js"],
        cli: {
            migrationsDir: "src/typeorm/migrations"
        }
    } as ConnectionOptions
);
export default connectionOptions;