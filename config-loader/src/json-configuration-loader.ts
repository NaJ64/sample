import { IConfigurationLoader } from "./configuration-loader";
import fs from "fs";
import path from "path";

export class JsonConfigurationLoader implements IConfigurationLoader {

    private readonly _environment: string | null;
    private readonly _fileName: string;
    private readonly _fileExtension: string;
    private readonly _rootDirectory: string;

    public constructor(environment?: string, fileName?: string, rootDirectory?: string) {
        this._environment = environment || process.env["NODE_ENV"] || null;
        this._fileName = fileName || "appsettings";
        this._fileExtension = "json";
        this._rootDirectory = rootDirectory || path.resolve("./");
    }

    public load(): Map<string, any> {
        let configuration: any = {};
        let configurationFiles = this.getConfigurationFiles();
        for (let configurationFile of configurationFiles) {
            let configurationFromJson = this.loadConfigurationFromFile(configurationFile);
            Object.assign(configuration, configurationFromJson);
        }
        let configurationMap = new Map(Object.entries(configuration));
        return configurationMap;
    }

    private loadConfigurationFromFile(filePath: string): any {
        let jsonContents = fs.readFileSync(filePath);
        return JSON.parse(jsonContents.toString());
    }

    private getConfigurationFiles(): string[] {

        // List of config files found in root directory
        let files: string[] = [];

        // Load base config first
        let baseFile = `${this._fileName}.${this._fileExtension}`;
        let baseFilePath = path.resolve(this._rootDirectory, baseFile);
        if (fs.existsSync(baseFilePath)) {
            files.push(baseFilePath);
        }

        // Load environment-specific config next
        let envFile = this._environment && `${this._fileName}.${this._environment}.${this._fileExtension}`;
        let envFilePath = envFile && path.resolve(this._rootDirectory, envFile);
        if (envFilePath && fs.existsSync(envFilePath)) {
            files.push(envFilePath);
        }

        // Return list of config files
        return files;

    }

}