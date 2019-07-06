import { IOptions as IInfrastructureOptions, Options as InfrastructureOptions } from "sample-infrastructure";

export interface IOptions extends IInfrastructureOptions { }

export class Options extends InfrastructureOptions implements IOptions { }