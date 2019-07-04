import { IUnitOfWork, IUnitOfWorkFactory } from "../abstractions/unit-of-work";
import { IParentRepository } from "./parent/parent-repository";

export interface ISampleUnitOfWork extends IUnitOfWork {
    parents: IParentRepository;
}

export interface ISampleUnitOfWorkFactory extends IUnitOfWorkFactory<ISampleUnitOfWork> { }