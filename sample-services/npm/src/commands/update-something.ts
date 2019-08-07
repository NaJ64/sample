import { ICommand } from "../abstractions/command";
import { CommandHandlerBase } from "../abstractions/command-handler";

export interface IUpdateSomethingCommand extends ICommand {
    someId: number;
    someNewData: string;
}

export class UpdateSomethingCommand implements IUpdateSomethingCommand {
    static readonly TYPE = "SampleServices.Commands.UpdateSomethingCommand";
    readonly type: string;
    someId: number;
    someNewData: string;
    constructor(state?: Partial<IUpdateSomethingCommand>) {
        this.someId = 0;
        this.someNewData = "default";
        state && Object.assign(this, JSON.parse(JSON.stringify(state)));
        this.type = UpdateSomethingCommand.TYPE;
    }
}

export interface IUpdateSomethingHandler extends CommandHandlerBase<IUpdateSomethingCommand, number> { }