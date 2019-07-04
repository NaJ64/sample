import { ICommand } from "../abstractions/command";
import { CommandHandlerBase } from "../abstractions/command-handler";

export class AddSomethingCommand implements ICommand {
    static readonly TYPE = "SampleServices.Commands.AddSomethingCommand";
    type = AddSomethingCommand.TYPE;
    someNewData: string;
    constructor() {
        this.someNewData = "default";
    }
}

export interface IAddSomethingHandler extends CommandHandlerBase<AddSomethingCommand, number> { }