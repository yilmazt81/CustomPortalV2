import React, { useState, useEffect, useRef } from "react";
import { CFormInput, CListGroup, CListGroupItem } from "@coreui/react";

export default function CoreUiAutoComplete({ suggestions,placeholder }) {
  const [text, setText] = useState("");
  const [filtered, setFiltered] = useState([]);
  const [showList, setShowList] = useState(false);
  const wrapperRef = useRef(null);

  // ↪ Liste dışına tıklayınca kapanması
  useEffect(() => {
    const handleClick = (e) => {
      if (wrapperRef.current && !wrapperRef.current.contains(e.target)) {
        setShowList(false);
      }
    };
    document.addEventListener("mousedown", handleClick);
    return () => document.removeEventListener("mousedown", handleClick);
  }, []);

  const onChange = (e) => {
    const value = e.target.value;
    setText(value);

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

  const onSelect = (item) => {
    setText(item);
    setShowList(false);
  };

  return (
    <div style={{ position: "relative", width: "100%" }} ref={wrapperRef}>
      <CFormInput
        placeholder={placeholder}
        value={text}
        onChange={onChange}
      />

      {showList && filtered.length > 0 && (
        <CListGroup
          style={{
            position: "absolute",
            width: "100%",
            zIndex: 999,
            maxHeight: "200px",
            overflowY: "auto",
            top: "100%",
            borderTopLeftRadius: 0,
            borderTopRightRadius: 0,
          }}
        >
          {filtered.map((item) => (
            <CListGroupItem
              key={item}
              component="button"
              onClick={() => onSelect(item)}
              style={{
                cursor: "pointer",
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
