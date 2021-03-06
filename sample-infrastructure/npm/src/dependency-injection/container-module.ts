import { ContainerModule, interfaces } from "inversify";
import { Options, IOptions } from "./options";
import { SampleOrmPersistenceModule } from "sample-infrastructure-persistence-orm";
import { SampleDomainServicesModule } from "sample-infrastructure-services-domain";

type ConfigureOptions = (configureOptions: IOptions) => void;

export class SampleInfrastructureModule extends ContainerModule {

    public constructor(configureOptions?: ConfigureOptions) {
        super(SampleInfrastructureModule.createBind(configureOptions));
    }

    public static createBind(configureOptions?: ConfigureOptions) {
        return (bind: interfaces.Bind) => {

            const options = new Options();
            configureOptions && configureOptions(options);

            if (options.domainCommands || options.domainQueries) {
                SampleDomainServicesModule.createBind(domainServices => {
                    domainServices.enableCommands = !!options.domainCommands;
                    domainServices.enableQueries = !!options.domainQueries;
                })(bind);
            }

            if (options.postgres) {
                SampleOrmPersistenceModule.createBind(ormPersistence => {
                    ormPersistence.postgres = options.postgres;
                })(bind);
            }

        };
    }

}