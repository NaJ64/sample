# `cert-loader`

> Provides async abstraction(s) for loading server certificates using Node.js fs

## Usage

```
const { FileCertificateLoader } = require('cert-loader');

(async () => {

    const cert = await new FileCertificateLoader()
        .loadAsync({
            path: "./certificates/mycert.pfx",
            password: "myPassw0rd"
        });

})();

```
