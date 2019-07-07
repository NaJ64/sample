import express from "express";
import { Container } from "inversify";

export interface IStartupLike {
    configure?(app: express.Application, services: Container, environment: string): express.Application;
    configureServices?(services: Container): void;
}

export type StartupLikeConstructor = {
    new (configuration: Map<string, any>, environment: string): IStartupLike;
}