import "reflect-metadata";
import { IWebHostBuilder, WebHost } from "expressify";
import { Startup } from "./startup";

export class Program {

    public static main(...args: any[]): void {
        Program.createWebHostBuilder(args).build().run();
    }

    public static createWebHostBuilder(...args: any[]): IWebHostBuilder {
        return WebHost.createDefaultBuilder(args)
            .useStartup(Startup);
    }

}