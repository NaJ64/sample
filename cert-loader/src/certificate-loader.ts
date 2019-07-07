import { ICertificateOptions } from "./certificate-options";

export interface ICertificateLoader {
  loadAsync(options: ICertificateOptions): Promise<Buffer>;
}