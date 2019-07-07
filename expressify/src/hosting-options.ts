import { IEndPointOptions } from "./end-point-options";

export interface IHostingOptions {
    endPoints: { [name: string]: IEndPointOptions };
}