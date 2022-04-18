const uri = 'api/Weather';

let maxPage = 0;

let currentPage = 0;

let Month = 0;

let Year = 0;

async function setMonth() {
	var selectBox = document.getElementById("selectMonth");
	var selectedValue = selectBox.options[selectBox.selectedIndex].value;
	Month = selectedValue;
	currentPage = 0;
	renderWeathers(currentPage, Year, Month);
}

async function setYear() {
	var selectBox = document.getElementById("selectYear");
	var selectedValue = selectBox.options[selectBox.selectedIndex].value;
	Year = selectedValue;
	currentPage = 0;
	renderWeathers(currentPage, Year, Month);
}

function getValue(value) {
	if (value === null) {
		return "";
	}
	else {
		return value;
    }
}

async function setMaxPage(count) {
	maxPage = Math.trunc(count / 10);
}

async function getCount() {
	let url = `${uri}/getCount`;
    try {
		let result = await fetch(url);
		return await result.json()
    } catch (e) {
		console.log(error);
    }

}

async function nextPage() {
	if (currentPage != maxPage) {
		currentPage += 1;
		renderWeathers(currentPage, Year, Month);
    }
}

async function prevPage() {
	if (currentPage != 0) {
		currentPage -= 1;
		renderWeathers(currentPage, Year, Month);
    }
}

async function getWeathers(currentPage, year, month) {
	let url = `${uri}/getWeather/${currentPage}?year=${year}&Month=${month}`;
    try {
		let result = await fetch(url);
		return await result.json();
    } catch (e) {
		console.log(error);
    }
}

async function renderWeathers(currentPage, year, month) {
	let result = await getWeathers(currentPage, year, month);
	setMaxPage(result.count);
	var weathers = result.weathers;
	let html =
		`<table class="table table-hover">
		<tr>
			<th>Дата</th>
			<th>Время Московское</th>
			<th>T</th>
			<th>Отн. влажность воздуха, %</th>
			<th>Атм. давление, мм рт.ст.</th>
			<th>Направление ветра</th>
			<th>Скорость ветра, mc/с</th>
			<th>Облачность, %</th>
			<th>h</th>
			<th>VV</th>
			<th>Погодные явления</th>
		</tr>`;
	weathers.forEach(row => {
		let htmlSegment =
			`<tr>
			<td>${row.date}</td>
			<td>${row.time}</td>
			<td>${getValue(row.airTemperature)}</td>
			<td>${getValue(row.relativeHumidity)}</td>
			<td>${getValue(row.atmospherePressure)}</td>
			<td>${row.windDirection}</td>
			<td>${getValue(row.windSpeed)}</td>
			<td>${getValue(row.cloudy)}</td>
			<td>${getValue(row.cloudLowBoundary)}</td>
			<td>${getValue(row.horizontalVsibility)}</td>
			<td>${getValue(row.weatherEvents)}</td>
			</tr>`;
		html += htmlSegment;
	});
	html += '</table>';
	let container = document.querySelector('.fetchTable');
	container.innerHTML = html;
}

renderWeathers(currentPage, Year, Month);

