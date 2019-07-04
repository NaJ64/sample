import { IDisposable } from "./disposable";

export interface IUnitOfWork extends IDisposable {
    beginAsync(): Promise<string>;
    commitAsync(): Promise<void>;
    rollbackAsync(): Promise<void>;
}

export interface IUnitOfWorkFactory<TUnitOfWork extends IUnitOfWork> {
    create(): TUnitOfWork;
}