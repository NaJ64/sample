import { ICertificateOptions } from "cert-loader";

export interface IEndPointOptions {
    url: string,
    certificate?: ICertificateOptions;
}
