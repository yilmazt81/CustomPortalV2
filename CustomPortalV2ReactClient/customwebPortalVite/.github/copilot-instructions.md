# Copilot Instructions for CustomPortalV2 React Client

## Architecture Overview

**Tech Stack**: React 19 + Vite + Redux + CoreUI + Material-UI + i18next

This is a business portal frontend for customs/logistics operations. Key architectural patterns:

- **Routing**: HashRouter-based with lazy-loaded route components (see `src/routes.js`)
- **State Management**: Simple Redux store (`src/store.js`) for UI state (e.g., `sidebarShow`); API responses managed locally in component state
- **Layout**: CoreUI-based responsive layout with `DefaultLayout` wrapping authenticated views; `UrlProvider` context for URL management
- **API Integration**: Axios-based with Bearer token authentication stored in localStorage as `"LastToken"`
- **Internationalization**: i18next with auto-detection; supports Turkish (default), English, Russian in `src/translation/`

## Directory Structure & Key Patterns

**API Layer** (`src/lib/`):
- `Apibase.jsx`: Helper functions `Post()`, `PostFile()`, `GetRequest()` that inject Bearer token from localStorage
- `companyapi.jsx`, `userapi.jsx`, `workflowApi.jsx`, etc.: Domain-specific API clients
- `axiosConfigs.jsx`: Central axios config with error interceptor (suppresses 401 logging)
- `axiosUtils.jsx`: Utility functions for HTTP operations
- `URLContext.jsx` + `UrlReducer.jsx`: Context-based URL state management

**View Components** (`src/views/`):
- Dashboard, branch/address/product/user definitions, digital forms, workflows
- Each view typically has: main component + `DataGrid.js` (MaterialReactTable config) + modal components for CRUD
- Pattern: Fetch data in `useEffect`, render MaterialReactTable grid with edit/delete/add actions

**Shared Components** (`src/components/`):
- `AppHeader.js`, `AppSidebar.js`, `AppContent.js`, `AppFooter.js`: Layout shell
- `DeleteModal.js`: Reusable delete confirmation dialog
- `CoreUiAutoComplete.js`: CoreUI-wrapped autocomplete
- `LoadingAnimation.js`: Loading spinner wrapper using Lottie
- `DynamicFaIcon.js`: Dynamic Font Awesome icon resolver

**Styling**:
- SCSS with Vite preprocessor (configured in `vite.config.js`)
- Uses CoreUI SCSS functions/variables via `@use` directives in `src/scss/style.scss`
- Theme files: `_variables.scss`, `_custom.scss`, `_layout.scss`

## Developer Workflows

**Development**:
```bash
npm run dev          # Start Vite dev server (HMR enabled)
npm run build        # Production build
npm run lint         # Run ESLint
npm run preview      # Preview production build locally
```

**Dependencies to Know**:
- `@coreui/react`: Base UI components (CCard, CButton, CTable, etc.)
- `@mui/x-data-grid`: Advanced data grid (wrapped as MaterialReactTable)
- `react-router-dom`: Client-side routing (HashRouter for hash-based URLs)
- `i18next`: Multi-language support
- `axios`: HTTP client with interceptors
- `redux`: Minimal Redux store (mainly for sidebar toggle state)

## Critical Integration Points & Conventions

**Authentication & API Calls**:
- Token stored in `localStorage.getItem("LastToken")`
- All API functions must call `GetRequest()`, `Post()`, or `PostFile()` which auto-inject the token
- If `LastToken` missing, redirect to login (see `DefaultLayout.js` pattern)
- API response structure: `{ returnCode: 1, data: [...] }` (returnCode 1 = success)

**View Component Pattern**:
```javascript
import { useTranslation } from "react-i18next";
import { MaterialReactTable, useMaterialReactTable } from 'material-react-table';
import { Gridcolumns } from './DataGrid';  // Column config
import { GetList, Save, Delete, Create } from '../../lib/[domain]api';

export default function ViewName() {
  const { t } = useTranslation();
  const [data, setData] = useState([]);
  
  useEffect(() => {
    GetList().then(res => setData(res.data));
  }, []);
  
  return <MaterialReactTable ... />;  // Use column def from DataGrid
}
```

**Multilingual UI**:
- Always use `useTranslation()` hook and `t('key')` for text
- Translation keys defined in `src/translation/[Language]/translations.js`
- Default language is Turkish; change in `i18.js` with `i18n.changeLanguage()`

**Grid & Modal Pattern** (branch/product/user definitions):
- `DataGrid.js` exports `Gridcolumns` array (MaterialReactTable column definitions)
- Edit/delete modals trigger API calls via `Save()`, `Delete()`
- Success → toast notification or refetch via `GetList()`
- Use `DeleteModal.js` for confirmation dialogs

**Path Aliases**:
- `@` resolves to `src/` (configured in `vite.config.js`)
- Use: `import AppBreadcrumb from '@/components/AppBreadcrumb'`

## Important Gotchas & Non-Standards

1. **Component Extensions**: Mix of `.js` (class/functional) and `.jsx` extensions used interchangeably—treat the same
2. **Lazy Loading**: Views are lazy-loaded with Suspense fallback; components inside routes must be wrapped with `React.lazy()`
3. **Context vs Redux**: URLContext is separate from main Redux store; don't conflate them
4. **No TypeScript**: Project uses `.js`/`.jsx` without TypeScript—maintain this convention
5. **localStorage Dependency**: Tight coupling to `localStorage.LastToken` for auth; session management is implicit

## When Adding Features

1. Add route in `src/routes.js` and update `src/_nav.js` navigation
2. Create view folder in `src/views/[featureName]/` with `index.js`, `DataGrid.js`, and modals
3. Add API functions to appropriate file in `src/lib/` (e.g., `featureapi.js`)
4. Add translation keys to all language files in `src/translation/`
5. Import API functions and use `GetRequest()`/`Post()` patterns—never use axios directly
6. Use MaterialReactTable for grids; reference existing DataGrid patterns
7. Test with `npm run build` to catch any async/import issues
