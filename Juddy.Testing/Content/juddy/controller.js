(function ($) {
	var handler = null,
			page = 1,
			isLoading = false,
			apiURL = 'http://www.wookmark.com/api/json/popular';

	// Prepare layout options.
	var options = {
		autoResize: true, // This will auto-update the layout when the browser window is resized.
		container: $('#tiles'), // Optional, used for some extra CSS styling
		offset: 2, // Optional, the distance between grid items
		itemWidth: 210 // Optional, the width of a grid item
	};

	/**
	 * When scrolled all the way to the bottom, add more tiles.
	 */
	function onScroll(event) {
		// Only check when we're not still waiting for data.
		if(!isLoading) {
			// Check if we're within 100 pixels of the bottom edge of the broser window.
			var closeToBottom = ($(window).scrollTop() + $(window).height() > $(document).height() - 100);
			if(closeToBottom) {
				loadData();
			}
		}
	};

	/**
	 * Refreshes the layout.
	 */
	function applyLayout() {
		options.container.imagesLoaded(function() {
			// Create a new layout handler when images have loaded.
			handler = $('#tiles li');
			handler.wookmark(options);
		});
	};

	/**
	 * Loads data from the API.
	 */
	function loadData() {
		isLoading = true;
		$('#loaderCircle').show();

		$.ajax({
			url: apiURL,
			dataType: 'jsonp',
			data: {page: page}, // Page parameter to make sure we load new data
			success: onLoadData
		});
	};

	/**
	 * Receives data from the API, creates HTML for images and updates the layout
	 */
	function onLoadData(data) {
		isLoading = false;
		$('#loaderCircle').hide();

		// Increment page index for future calls.
		page++;

		// Create HTML for the images.
		var html = '';
		var i=0, length=data.length, image;
		for(; i<length; i++) {
			image = data[i];
			html += '<li>';

			// Image tag (preview in Wookmark are 200px wide, so we calculate the height based on that).
			html += '<img src="'+image.preview+'" width="200" height="'+Math.round(image.height/image.width*200)+'">';

			// Image title.
			html += '<p>'+image.title+'</p>';

			html += '</li>';
		}

		// Add image HTML to the page.
		$('#tiles').append(html);

		// Apply layout.
		applyLayout();
	};

	// Capture scroll event.
	$(document).bind('scroll', onScroll);

	// Load first data from the API.
	loadData();
})(jQuery);
