import React, { createContext, useReducer } from 'react';
import UrlReducer from './UrlReducer';

// 1. Context Oluştur
const UrlContext = createContext();

// 2. Context Provider Bileşeni
const UrlProvider = ({ children }) => {
  const [state, dispatch] = useReducer(UrlReducer, []);

  return (
    <UrlContext.Provider value={{ state, dispatch }}>
      {children}
    </UrlContext.Provider>
  );
};

export    { UrlProvider, UrlContext };


