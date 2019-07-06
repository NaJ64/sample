import { ContainerModule, interfaces } from "inversify";
import { IAddSomethingHandler, IQueries, IRemoveSomethingHandler, IUpdateSomethingHandler, TYPES as SampleServices } from "sample-services";
import { DomainAddSomethingHandler } from "../commands/add-something";
import { DomainRemoveSomethingHandler } from "../commands/remove-something";
import { DomainUpdateSomethingHandler } from "../commands/update-something";
import { DomainQueries } from "../queries/queries";
import { IOptions, Options } from "./options";
import { SampleDomainModule } from "sample-domain";

type ConfigureOptions = (configureOptions: IOptions) => void;

export class SampleDomainServicesModule extends ContainerModule {

    public constructor(configureOptions?: ConfigureOptions) {
        super(SampleDomainServicesModule.createBind(configureOptions));
    }

    public static createBind(configureOptions?: ConfigureOptions) {
        return (bind: interfaces.Bind) => {

            const options = new Options();
            configureOptions && configureOptions(options);

            if (options.enableCommands || options.enableQueries) {
                SampleDomainModule.createBind()(bind);
            }

            if (options.enableCommands) {

                //IAddSomethingHandler
                bind<IAddSomethingHandler>(SampleServices.Commands.IAddSomethingHandler)
                    .to(DomainAddSomethingHandler)
                    .inTransientScope();
                    
                //IUpdateSomethingHandler
                bind<IUpdateSomethingHandler>(SampleServices.Commands.IUpdateSomethingHandler)
                    .to(DomainUpdateSomethingHandler)
                    .inTransientScope();

                //IRemoveSomethingHandler
                bind<IRemoveSomethingHandler>(SampleServices.Commands.IRemoveSomethingHandler)
                    .to(DomainRemoveSomethingHandler)
                    .inTransientScope();
            }

            if (options.enableQueries) {

                //IQueries
                bind<IQueries>(SampleServices.IQueries)
                    .to(DomainQueries)
                    .inTransientScope();

            }

        };
    }
    
}