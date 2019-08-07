import { ICommand } from "../abstractions/command";
import { CommandHandlerBase } from "../abstractions/command-handler";

export interface IAddSomethingCommand extends ICommand {
    someNewData: string;
}

export class AddSomethingCommand implements IAddSomethingCommand {
    static readonly TYPE = "SampleServices.Commands.AddSomethingCommand";
    readonly type: string;
    someNewData: string;
    constructor(state?: Partial<IAddSomethingCommand>) {
        this.someNewData = "default";
        state && Object.assign(this, JSON.parse(JSON.stringify(state)));
        this.type = AddSomethingCommand.TYPE;
    }
}

export interface IAddSomethingHandler extends CommandHandlerBase<IAddSomethingCommand, number> { }