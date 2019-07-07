import fs from "fs";
import path from "path";
import { ICertificateLoader } from "./certificate-loader";
import { ICertificateOptions } from "./certificate-options";

export class FileCertificateLoader implements ICertificateLoader {

  public async loadAsync(options: ICertificateOptions) {
    let fromFile = !!options.path;
    if (!fromFile) {
      throw new Error("Certificate options must specify a file path when using the current loader");
    }
    let loadedCert = await this.loadFromFileAsync(options);
    return loadedCert;
  }

  private async loadFromFileAsync(options: ICertificateOptions): Promise<Buffer> {
    if (!fs.existsSync(path.resolve(options.path || ""))) {
      throw new Error("Could not locate file from path");
    }
    return await new Promise<Buffer>((resolve, reject) => {
      try {
        fs.readFile(path.resolve(options.path || ""), (err: NodeJS.ErrnoException | null, data: Buffer) => {
          if (err) {
            throw err;
          } else {
            resolve(data);
          }
        });
      } catch (ex) {
        reject(ex);
      }
    });
  }

}
