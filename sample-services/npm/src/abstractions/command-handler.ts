import { ICommand } from './command';

export interface ICommandHandler<TCommand extends ICommand, TResult> {
    commandType: string;
    handleAsync(command: TCommand): Promise<TResult>;
}

export abstract class CommandHandlerBase<TCommand extends ICommand, TResult> implements ICommandHandler<TCommand, TResult> {
    abstract commandType: string;
    abstract handleAsync(command: TCommand): Promise<TResult>;
}