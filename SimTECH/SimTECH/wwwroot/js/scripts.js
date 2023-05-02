// NOTE: This is not going to work on atleast Firefox since it doesn't allow blobs to be instantly copied to the clipboard
// Above is true until you set a specific config setting to true, very useful!
window.takeScreenshot = (target) => {
	var targetElement = document.getElementById(target);

	domtoimage.toBlob(targetElement)
		.then(function (blob) {
			navigator.clipboard.write([
				new ClipboardItem({
					[blob.type]: blob
				})
			]).then(() => { console.log("Copied html canvas!"); });
		});
}
