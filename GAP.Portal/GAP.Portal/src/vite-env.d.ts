/// <reference types="vite/client" />

interface ImportMetaEnv {
    readonly VITE_GAP_API_BASE_URL: string;
    // more env variables...
  }

  interface ImportMeta {
    readonly env: ImportMetaEnv
  }
