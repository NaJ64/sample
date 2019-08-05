import { JsonConfigurationLoader } from "config-loader";
import { SampleOrmPersistenceModule } from "sample-infrastructure-persistence-orm";
import { ConnectionOptions, EntitySchema } from "typeorm";

const postgresConfig = new JsonConfigurationLoader().load().get("postgres");
let connectionOptions: ConnectionOptions = SampleOrmPersistenceModule.createPostgresConnectionOptions(postgresConfig);
connectionOptions = Object.assign(
    SampleOrmPersistenceModule.createPostgresConnectionOptions(postgresConfig), 
    {
        entities: (connectionOptions.entities || []).map(x => {
            // Hack fix for "instanceof" type differing between typeorm module imports
            return new EntitySchema((x as EntitySchema).options);
        }),
        migrationsTableName: "__TypeORMMigrationsHistory",
        migrations: ["dist/typeorm/migrations/*.js"],
        cli: {
            migrationsDir: "src/typeorm/migrations"
        }
    } as ConnectionOptions
);
module.exports = connectionOptions;