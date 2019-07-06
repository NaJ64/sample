import { ICommand } from "../abstractions/command";
import { CommandHandlerBase } from "../abstractions/command-handler";

export class RemoveSomethingCommand implements ICommand {
    static readonly TYPE = "SampleServices.Commands.RemoveSomethingCommand";
    type = RemoveSomethingCommand.TYPE;
    someId: number;
    constructor() {
        this.someId = 0;
    }
}

export interface IRemoveSomethingHandler extends CommandHandlerBase<RemoveSomethingCommand, number> { }