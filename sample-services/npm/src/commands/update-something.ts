import { ICommand } from "../abstractions/command";
import { CommandHandlerBase } from "../abstractions/command-handler";

export class UpdateSomethingCommand implements ICommand {
    static readonly TYPE = "SampleServices.Commands.UpdateSomethingCommand";
    type = UpdateSomethingCommand.TYPE;
    someId: number;
    someNewData: string;
    constructor() {
        this.someId = 0;
        this.someNewData = "default";
    }
}

export interface IUpdateSomethingHandler extends CommandHandlerBase<UpdateSomethingCommand, number> { }