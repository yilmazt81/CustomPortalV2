import i18n from "i18next";
import { initReactI18next } from "react-i18next";
import LanguageDetector from "i18next-browser-languagedetector";

import { TRANSLATIONS_TR } from "./Turkish/translations";
import { TRANSLATIONS_EN } from "./English/translations";
import { TRANSLATIONS_RU } from "./Russian/translations";

i18n
 .use(LanguageDetector)
 .use(initReactI18next)
 .init({
   resources: {
     en: {
       translation: TRANSLATIONS_EN
     },
     tr: {
       translation: TRANSLATIONS_TR
     },
     ru: {
       translation: TRANSLATIONS_RU
     }
   }
 });
 
i18n.changeLanguage("tr");


export default i18n;


