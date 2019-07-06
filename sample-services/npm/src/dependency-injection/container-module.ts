import { ContainerModule, interfaces } from "inversify";
import { IOptions, Options } from "./options";

type ConfigureOptions = (configureOptions: IOptions) => void;

export class SampleServicesModule extends ContainerModule {

    public constructor(configureOptions?: ConfigureOptions) {
        super(SampleServicesModule.createBind(configureOptions));
    }

    public static createBind(configureOptions?: ConfigureOptions) {
        return (bind: interfaces.Bind) => {

            const options = new Options();
            configureOptions && configureOptions(options);

        };
    }
    
}