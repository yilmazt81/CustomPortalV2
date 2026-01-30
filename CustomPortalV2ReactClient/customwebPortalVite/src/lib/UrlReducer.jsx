const UrlReducer = (state, action) => {
    switch (action.type) {
      case 'Add':
        return [...state, { pathname: action.payload.pathname, name: action.payload.name,active:action.payload.active }]; 
      case 'reset':
        return [];
      default:
        throw new Error(`Unknown action: ${action.type}`);
    }
  };
  
  export default UrlReducer;


