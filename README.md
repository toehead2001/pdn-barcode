# Barcode
Paint.net plugin to create POSTNET and Code 39 barcodes This is a bug fix release of Sepcot's old 'Barcode' plugin. See changelog below.
 

![UI](http://kalama.ml/gfx/barcode/ui.png)

![Code 39](http://kalama.ml/gfx/barcode/barcode-39.png)

'getpaint.net' encoded with Code 39

![POSTNET](http://kalama.ml/gfx/barcode/barcode-post.png)

'93901' Postal Code encoded with POSTNET


##Features

* Supports all three Code 39 barcode encodings: Code 39, Code 39 mod 43, and Full ASCII Code 39
* Supports 5, 6, 9, and 11 digit POSTNET encodings
* Automatically tries to convert text into an encodable format. (Example: "Paint" in Code 39 becomes "PAINT")
* Provides visual feedback for text that cannot be encoded.
* Uses the user defined Primary and Secondary colors to build the barcodes. Note: Barcodes built with colors other than Black (Primary) and White (Secondary) will probably not scan correctly.
* The width of the bars in the barcode adjust to fill the available space. No barcode is shown if the surface is too small for the barcode to be completely drawn.
* Supports non-rectangular selections. Note: Barcodes may not scan correctly if not rectangular.


##Changelog

v1.2 by toe_head2001 (Feb 23, 2015)

* Fixed: Plugin would crash if Unicode characters were inputted into the text field (info)
* Fixed: Character validation was not functional on the 'Full ASCII Code 39' option
* Changed: Live preview and the 'OK' button are now disabled if invalid characters are inputted
* Changed: Menu entry in now under the 'Render' submenu
* New: Text cursor is now automatically placed in the text input field when the plugin is opened
* New: Metadata was added for the 'Plugin Browser'

v1.1 by Sepcot (March 19, 2007)

* Supports 5, 6, 9, and 11 digit POSTNET encodings
* Supports non-rectangular selections.

v1.0 by Sepcot (March 15, 2007)

* Initial release


##Links
* [Paint.NET plugin forum thread](http://forums.getpaint.net/index.php?showtopic=31559)
