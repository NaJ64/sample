import { ICommand } from "../abstractions/command";
import { CommandHandlerBase } from "../abstractions/command-handler";

export interface IRemoveSomethingCommand extends ICommand {
    someId: number;
}

export class RemoveSomethingCommand implements IRemoveSomethingCommand {
    static readonly TYPE = "SampleServices.Commands.RemoveSomethingCommand";
    readonly type: string;
    someId: number;
    constructor(state?: Partial<IRemoveSomethingCommand>) {
        this.someId = 0;
        state && Object.assign(this, JSON.parse(JSON.stringify(state)));
        this.type = RemoveSomethingCommand.TYPE;
    }
}

export interface IRemoveSomethingHandler extends CommandHandlerBase<IRemoveSomethingCommand, number> { }