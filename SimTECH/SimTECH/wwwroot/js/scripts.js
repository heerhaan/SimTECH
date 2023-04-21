// NOTE: This is not going to work on atleast Firefox since it doesn't allow blobs to be instantly copied to the clipboard
// Above is true until you set a specific config setting to true, very useful!
window.takeScreenshot = (target) => {
	domtoimage.toCanvas(document.getElementById(target))
		.then(function (canvas) {
			canvas.toBlob(blob => {
				navigator.clipboard.write([
					new ClipboardItem({
						[blob.type]: blob
					})
				]).then(() => { console.log("Copied html canvas!"); })
			});
		}
	);
}
