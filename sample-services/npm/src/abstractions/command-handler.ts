export interface ICommandHandler<TCommand, TResult=void> {
    commandType: string;
    handleAsync(command: TCommand): Promise<TResult>;
}

export abstract class CommandHandlerBase<TCommand, TResult=void> implements ICommandHandler<TCommand, TResult> {
    abstract commandType: string;
    abstract handleAsync(command: TCommand): Promise<TResult>;
}