import { ContainerModule, interfaces } from "inversify";
import { ISampleUnitOfWorkFactory, TYPES as SampleDomain } from "sample-domain";
import { ConnectionOptions } from "typeorm";
import { ChildSchema } from "../typeorm/aggregates/parent/child-schema";
import { ParentSchema } from "../typeorm/aggregates/parent/parent-schema";
import { SampleUnitOfWorkFactory } from "../typeorm/aggregates/sample-unit-of-work";
import { IOptions, IPostgresConnection, Options } from "./options";
import { TYPES as SampleOrmPersistence } from "./types";

type ConfigureOptions = (configureOptions: IOptions) => void;

export class SampleOrmPersistenceModule extends ContainerModule {

    public constructor(configureOptions?: ConfigureOptions) {
        super(SampleOrmPersistenceModule.createBind(configureOptions));
    }

    public static createBind(configureOptions?: ConfigureOptions) {
        return (bind: interfaces.Bind) => {

            const options = new Options();
            configureOptions && configureOptions(options);

            if (!options.postgres) {
                throw new Error("PostgreSQL connection options must be provided");
            }

            //ConnectionOptions
            const postgresConnectionOptions = SampleOrmPersistenceModule.createPostgresConnectionOptions(options.postgres);
            bind<ConnectionOptions>(SampleOrmPersistence.TypeORM.ConnectionOptions)
                .toConstantValue(postgresConnectionOptions);

            //IAddSomethingHandler
            bind<ISampleUnitOfWorkFactory>(SampleDomain.Aggregates.ISampleUnitOfWorkFactory)
                .to(SampleUnitOfWorkFactory)
                .inSingletonScope();

        };
    }

    public static createPostgresConnectionOptions(postgres: IPostgresConnection): ConnectionOptions {
        return <ConnectionOptions>{
            ...postgres,
            type: "postgres",
            entities: [
                ParentSchema,
                ChildSchema
            ]
        };
    }

}