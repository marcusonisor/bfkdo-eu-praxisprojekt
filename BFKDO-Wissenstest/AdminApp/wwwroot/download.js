function downloadFile(filename, data) {
    var a = document.createElement("a");
    a.style.display = "none";
    document.body.appendChild(a);
    a.href = window.URL.createObjectURL(new Blob([data], { type: "octet/stream" }));
    a.setAttribute("download", filename);
    a.click();
    window.URL.revokeObjectURL(a.href);
    document.body.removeChild(a);
}