import { IAsyncDisposable } from "./disposable";

export interface IUnitOfWork extends IAsyncDisposable {
    beginAsync(): Promise<string>;
    commitAsync(): Promise<void>;
    rollbackAsync(): Promise<void>;
}

export interface IUnitOfWorkFactory<TUnitOfWork extends IUnitOfWork> {
    create(): TUnitOfWork;
}