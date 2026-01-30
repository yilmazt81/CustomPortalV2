import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import path from 'path';

export default defineConfig({
  plugins: [react()],
    css: {
    preprocessorOptions: {
      scss: {
        additionalData: `
        @use "@coreui/coreui/scss/functions" as *;
        @use "@coreui/coreui/scss/variables" as *;
        @use "@coreui/coreui/scss/maps" as *;
        @use "@coreui/coreui/scss/mixins" as *;
        `
      }
    }
  },
  resolve: {
    alias: {
      '@': path.resolve(__dirname, 'src'),
    },
  },
  
});
