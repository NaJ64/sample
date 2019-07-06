import { ContainerModule, interfaces } from "inversify";
import { SampleInfrastructureModule } from "sample-infrastructure";
import { SampleServicesModule } from "sample-services";
import { IOptions, Options } from "./options";

type ConfigureOptions = (configureOptions: IOptions) => void;

export class SampleApiModule extends ContainerModule {

    public constructor(configureOptions?: ConfigureOptions) {
        super(SampleApiModule.createBind(configureOptions));
    }

    public static createBind(configureOptions?: ConfigureOptions) {
        return (bind: interfaces.Bind) => {

            const options = new Options();
            configureOptions && configureOptions(options);

            SampleServicesModule.createBind()(bind);

            SampleInfrastructureModule.createBind(infrastructure => 
                Object.assign(infrastructure, options)
            )(bind);

        }
    }



}