db.createUser({
    user: "DistriSystemAdmin",
    pwd: "7yW59G6EokFU",
    roles: [{
        role: "readWrite",
        db: "DistriSystemdb"
    }
    ],
    mechanisms: ["SCRAM-SHA-1"]
});

db.createCollection("Orders");
