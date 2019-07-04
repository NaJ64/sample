export interface ICommand {
    readonly type: string;
}

export abstract class CommandBase implements ICommand {
    abstract type: string;
}