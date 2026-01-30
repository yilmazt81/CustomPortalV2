import React, { useState, useEffect, useRef } from "react";
import { CFormInput, CListGroup, CListGroupItem } from "@coreui/react";

// 1. Add 'name' and 'onChange' (instead of onValueChange)
export default function CoreUiAutoComplete({ suggestions, placeholder, name, onChange }) {
  const [text, setText] = useState("");
  const [filtered, setFiltered] = useState([]);
  const [showList, setShowList] = useState(false);
  const wrapperRef = useRef(null);

  useEffect(() => {
    const handleClick = (e) => {
      if (wrapperRef.current && !wrapperRef.current.contains(e.target)) {
        setShowList(false);
      }
    };
    document.addEventListener("mousedown", handleClick);
    return () => document.removeEventListener("mousedown", handleClick);
  }, []);

  // SCENARIO A: User Types (Real Event)
  const handleInputChange = (e) => {
    const value = e.target.value;
    setText(value);

    // Pass the real event object up to the parent
    if (onChange) {
      onChange(e);
    }

    if (!value.trim()) {
      setFiltered([]);
      setShowList(false);
      return;
    }

    const filteredData = suggestions.filter((item) =>
      item.toLowerCase().includes(value.toLowerCase())
    );

    setFiltered(filteredData);
    setShowList(true);
  };

  // SCENARIO B: User Selects from List (Synthetic Event)
  const onSelect = (item) => {
    setText(item);
    setShowList(false);

    // We must MANUALLY create an event-like object because 
    // clicking a <li> is not an input change event.
    if (onChange) {
      const syntheticEvent = {
        target: {
          name: name, // The name prop passed from parent
          value: item // The selected text
        }
      };
      onChange(syntheticEvent);
    }
  };

  return (
    <div style={{ position: "relative", width: "100%" }} ref={wrapperRef}>
      <CFormInput
        name={name} // Attach name here
        placeholder={placeholder}
        value={text}
        onChange={handleInputChange} // Use local handler
        autoComplete="off"
      />

      {showList && filtered.length > 0 && (
        <CListGroup
          style={{
            position: "absolute", // Akıştan çıkarır, alttakileri itmez
            top: "100%",          // Inputun tam altına yapıştırır
            left: 0,
            width: "100%",        // Genişlik input ile aynı olur
            zIndex: 100050,         // Diğer öğelerin üzerinde görünmesini sağlar
            maxHeight: "200px",   // Çok uzarsa scroll çıkar
            overflowY: "auto",
            backgroundColor: "#fff", // ÖNEMLİ: Arkadaki yazıların görünmesini engeller
            border: "1px solid #ced4da", // Input border'ı ile uyumlu renk
            borderTop: "none",    // Input ile birleşmiş gibi görünür
            borderRadius: "0 0 5px 5px",
            boxShadow: "0 4px 6px rgba(0,0,0,0.1)" // Derinlik katar
          }}
        >
          {filtered.map((item) => (
            <CListGroupItem
              key={item}
              component="button" // Important for accessibility
              type="button"      // Prevent form submission
              onClick={() => onSelect(item)}
              style={{
                cursor: "pointer",
                zIndex: 100060,
                textAlign: "left", // Metni sola hizala
                border: "none",    // Liste çizgilerini temizle
                borderBottom: "1px solid #f0f0f0" // Hafif ayırıcı çizgi
              }}
            >
              {item}
            </CListGroupItem>
          ))}
        </CListGroup>
      )}
    </div>
  );
}


