import { ISomethingDto } from "./models/something-dto";

export * from "./models";

export interface IQueries {
    getSomethingsAsync(): Promise<ISomethingDto[]>;
    getSomethingByIdAsync(id: number): Promise<ISomethingDto | null>;
}