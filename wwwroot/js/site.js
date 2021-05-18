google.charts.load('current', { packages: ['corechart'] });
google.charts.setOnLoadCallback(drawRatingChart);
google.charts.setOnLoadCallback(drawVisitorsCountChart);
google.charts.setOnLoadCallback(drawVisitorsByRegionChart);

function drawRatingChart() {
	$.get('/statistics/getRatingData')
		.done(function (response) {
			var rows = [['Оценка', 'Количество']]

			response.data.forEach((value) => rows.push(value));

			var data = google.visualization.arrayToDataTable(rows);

			var options = {
				legend: 'none',
				title: 'Оценки',
				hAxis: {
					minValue: 0,
					maxValue: response.max + 1,
					textPosition: 'none'
				}
			}

			var view = new google.visualization.DataView(data);
			var chart = new google.visualization.BarChart(document.getElementById("ratingChart"));
			chart.draw(view, options);
		});
}

function drawVisitorsCountChart() {
	$.get('/statistics/getVisitorsCount')
		.done(function (response) {
			var rows = [['Месяц', 'Посетители']]

			response.data.forEach((value) => rows.push(value));

			var data = google.visualization.arrayToDataTable(rows);

			var options = {
				width: $('#visCountChar').width(),
				legend: 'none',
				title: 'Количество посетителей',
				vAxis: {
					minValue: 0,
					maxValue: response.max + 1,
				}
			}

			var view = new google.visualization.DataView(data);
			var chart = new google.visualization.ColumnChart(document.getElementById("visCountChart"));
			chart.draw(view, options);
		});
}

function drawVisitorsByRegionChart() {
	$.get('statistics/getVisitorsByRegion')
		.done((response) => {
			var arrays = [['Регион', 'Посетители']];
			response.forEach((value) => arrays.push(value));

			var data = google.visualization.arrayToDataTable(arrays);
			var view = new google.visualization.DataView(data);
			var chart = new google.visualization.PieChart(document.getElementById('visitorsByRegion'));
			chart.draw(view, null);
		});
}

$(document).ready(function () {
	$('#visCountForm').on('submit', function () {
		var formData = {
			from: $('input[name=from]').val(),
			to: $('input[name=to]').val()
		};

		$.get('/statistics/getVisCountByInterval', formData)
			.done((response) => {
				console.log(response);
				$('#formResponse').show();
				$('#formResponse label:nth-child(1)').html(`Посещений: ${response.totalCount} | 
															Новые посетители: ${response.newVisitorsCount} | 
															Вернувшиеся посетители: ${response.oldVisitorsCount}`);
			});
		return false;
	});
})